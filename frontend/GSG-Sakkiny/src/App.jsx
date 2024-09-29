import React from "react";
import {
  Login,
  Signup,
  Error404,
  Home,
  ApartmentDetails,
  Dashboard,
  AccountSettings,
  AddApartment,
} from "./views";
import {
  BrowserRouter as Router,
  Routes,
  Route,
  Navigate,
} from "react-router-dom";
import { AuthProvider } from "./context/AuthProvider";
import { Toaster } from "react-hot-toast";
import RequireAuth from "./components/RequireAuth.js/RequireAuth";

const App = () => {
  return (
    <Router>
      <Toaster />
      <AuthProvider>
        <Routes>
          {/* Open Routes */}
          <Route path="/" element={<Home />} />
          <Route path="/home" element={<Navigate to="/" replace />} />
          <Route path="/login" element={<Login />} />
          <Route path="/signup" element={<Signup />} />
          <Route path="/apartments/:id" element={<ApartmentDetails />} />

          {/* Protected Routes */}
          <Route element={<RequireAuth />}>
            <Route path="/dashboard" element={<Dashboard />} />
            <Route path="/settings" element={<AccountSettings />} />
            <Route path="/addApartment" element={<AddApartment />} />
          </Route>
          <Route path="*" element={<Error404 />} />
        </Routes>
      </AuthProvider>
    </Router>
  );
};

export default App;
