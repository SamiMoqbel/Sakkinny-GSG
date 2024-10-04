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
  EditApartment,ApartmentRentalContract,
  Apartments
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
          <Route path="/editApartment" element={<EditApartment />} />
          <Route path="/addApartment" element={<AddApartment />} />
          <Route path="/apartmentRentalContract" element={<ApartmentRentalContract />} />

          {/* Protected Routes */}
          <Route element={<RequireAuth />}>
            <Route path="/dashboard" element={<Dashboard />} />
            <Route path="/settings" element={<AccountSettings />} />
            <Route path="*" element={<Error404 />} />
            <Route path="/addApartment" element={<AddApartment />} />
            {/* <Route path="/editApartment" element={<EditApartment />} /> */}
         
          </Route>
          <Route path="*" element={<Error404 />} />
        </Routes>
      </AuthProvider>
    </Router>
  );
};

export default App;
