import React, {useContext} from "react";
import {UpdatesContext} from "../context/UpdatesContext";
import {Widget} from "./Widget";

export const WidgetContainer : React.FC = () => {
    const { activeAlert, nextAlert } = useContext(UpdatesContext)

    return activeAlert 
        ? <Widget name={activeAlert.senderName} text={activeAlert.text} sound={activeAlert.sound} onEnd={nextAlert}/>
        : <></>
}