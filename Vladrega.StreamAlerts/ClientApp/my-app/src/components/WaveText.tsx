import React from "react";

type WaveTextProps = {
    text: string
}

export const WaveText : React.FC<WaveTextProps> = ({ text }) => {
    const letters = []

    for (let i = 0; i < text.length; i++) {
        const letter = text[i]
        letters.push(<span key={`${i}_${letter}`}>{letter}</span>)
    }

    return <>
        {letters}
    </>
} 