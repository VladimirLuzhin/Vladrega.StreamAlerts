import React, {PropsWithChildren, useCallback, useEffect, useMemo, useState} from "react"
import {HttpTransportType, HubConnectionBuilder, LogLevel} from '@microsoft/signalr'

export type Alert = {
    senderName: string,
    text: string,
    sound: string,
    isActive: boolean
}

interface IUpdatesContext
{
    activeAlert: Alert | undefined,
    nextAlert: () => void
}

export const UpdatesContext = React.createContext<IUpdatesContext>({
    activeAlert: undefined,
    nextAlert: () => {
        throw new Error('Context not initialized')
    }
})


const updatesMethodName = 'Updates'
const token = new URLSearchParams(document.location.search).get('token') ?? ''

const connection = new HubConnectionBuilder()
    .withUrl(`http://localhost:5006/updates`, {
        transport: HttpTransportType.WebSockets,
        accessTokenFactory(): string | Promise<string> {
            return token
        }
    })
    .withAutomaticReconnect()
    .configureLogging(LogLevel.Information)
    .build();

export const UpdatesContextProvider : React.FC<PropsWithChildren> = ({ children }) => {
    const [alerts, setAlerts] = useState<Alert[]>([])
    
    useEffect(() => {
        connection.on(updatesMethodName, (args) => {
            setAlerts(exist => {
                const copy = [...exist]
                copy.push(args as Alert)
                
                return copy
            })
        })
    }, [])
    
    useEffect(() => {
        connection.start()
            .then(() => console.log('connected'))
            .catch(e => console.log(e))
    }, [])
    
    useEffect(() => {
        if (alerts.length === 0)
            return
        
        if (alerts.some(a => a.isActive))
            return;
        
        setTimeout(() => {
            setAlerts(exist => {
                const copy = [...exist]
                if (copy.length > 0) {
                    const alert = copy[0]
                    alert.isActive = true
                }
                    
                
                return copy;
            })
        }, 2_000)
            
    }, [alerts])
   
    const activeAlert = useMemo(() => {
        if (alerts.length === 0)
            return undefined
        
        return alerts.find(a => a.isActive)
    }, [alerts])
    
    const nextAlert = useCallback(() => {
        setAlerts(exist => {
            const copy = [...exist]
            copy.shift()

            return copy
        })
    }, [alerts])
    
    return <UpdatesContext.Provider value={{
        activeAlert: activeAlert,
        nextAlert: nextAlert
    }}>
        {children}
    </UpdatesContext.Provider>
}