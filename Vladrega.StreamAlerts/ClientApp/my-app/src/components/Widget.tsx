import React, {useRef} from "react";
import './Widget.css'
import {WaveText} from "./WaveText";

type WidgetProps = {
    name: string,
    text: string,
    sound: string,
    onEnd: () => void
}

export const Widget : React.FC<WidgetProps> = ({ name, text, sound, onEnd  }) => {
    const ref = useRef<HTMLDivElement>(null)
    
    return <div ref={ref} className={'widget'}>
        <div className={'widget-name'}><WaveText text={name} /></div>
        <div className={'widget-text'}>{text}</div>
        <audio src={`data:audio/wav;base64,${sound}`} autoPlay onEnded={onEnd}/>
    </div>
}