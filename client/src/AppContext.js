import React from "react"

const AppContext = React.createContext()

export default AppContext
export const useAppContext = () => React.useContext(AppContext);