import React from 'react';
import {UpdatesContextProvider} from "./context/UpdatesContext";
import {WidgetContainer} from "./components/WidgetContainer";

export const App: React.FC = () =>  {
  return <UpdatesContextProvider>
    <WidgetContainer />
  </UpdatesContextProvider>
}
