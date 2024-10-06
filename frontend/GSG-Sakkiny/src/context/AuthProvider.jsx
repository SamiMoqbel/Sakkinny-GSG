import { createContext, useEffect, useLayoutEffect, useState } from "react";

const AuthContext = createContext();

export const AuthProvider = ({ children }) => {
  const localAuth = localStorage.getItem("userData");
  const [authenticated, setAuthenticated] = useState(JSON.parse(localAuth));

  useLayoutEffect(() => {
    if (localAuth) {
      setAuthenticated(JSON.parse(localAuth));
    }
  }, [localAuth]);

  return (
    <AuthContext.Provider value={{ authenticated, setAuthenticated }}>
      {children}
    </AuthContext.Provider>
  );
};

export default AuthContext;
