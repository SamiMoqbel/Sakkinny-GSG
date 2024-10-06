import { useState } from "react";
export const ApartmentRentalContract = () => {
  const [ownerDetails, setOwnerDetails] = useState({
    owner: "ABC Apartments",
    address: "123 Main Street, Blue City CA 55555",
    phoneNumber: "(555) 278-4476",
    email: "abcapartments@info.com",
  });

  const [renterDetails, setRenterDetails] = useState({
    renter: "Charlotte Thompson",
    address: "3191 Florence Street, Athens, TX 75751",
    phoneNumber: "(555) 887-6543",
    email: "charlottethompson@info.com",
  });

  const [apartmentDetails, setApartmentDetails] = useState({
    apartment: "Luxury Apartment",
    roomNum: "3",
    price: "$1,500/month",
  });

  // Handle input change
  const handleOwnerChange = (e) => {
    const { name, value } = e.target;
    setOwnerDetails((prevState) => ({ ...prevState, [name]: value }));
  };

  const handleRenterChange = (e) => {
    const { name, value } = e.target;
    setRenterDetails((prevState) => ({ ...prevState, [name]: value }));
  };

  const handleApartmentChange = (e) => {
    const { name, value } = e.target;
    setApartmentDetails((prevState) => ({ ...prevState, [name]: value }));
  };

  return (
    <div className="w-4/5 mx-auto my-5 bg-white rounded-lg shadow-lg p-6">
      <header className="flex justify-between items-center bg-red-600 py-4 px-6 border-b-2 border-gray-200">
        <h1 className="text-2xl font-bold text-gray-100">
          Apartment Rental Contract
        </h1>
      </header>

      <div className="p-6">
        <section className="mt-4">
          <h2 className="text-xl font-semibold mb-4">Information</h2>

          {/* Owner Table */}
          <table className="w-full table-auto mb-6">
            <tbody>
              <tr>
                <th className="p-2 text-left bg-red-600 w-[450px] text-gray-100">
                  Owner
                </th>
                <td className="p-2">
                  <input
                    type="text"
                    name="owner"
                    value={ownerDetails.owner}
                    onChange={handleOwnerChange}
                    className="w-full border border-gray-300 rounded px-3 py-2"
                  />
                </td>
              </tr>
              <tr className="bg-gray-50">
                <th className="p-2 text-left bg-gray-100 w-[450px] text-red-600">
                  Email
                </th>
                <td className="p-2">
                  <input
                    type="email"
                    name="email"
                    value={ownerDetails.email}
                    onChange={handleOwnerChange}
                    className="w-full border border-gray-300 rounded px-3 py-2"
                  />
                </td>
              </tr>
            </tbody>
          </table>

          {/* Renter Table */}
          <table className="w-full table-auto mb-6">
            <tbody>
              <tr>
                <th className="p-1 text-left bg-red-600 w-[450px] text-gray-100">
                  Renter
                </th>
                <td className="p-2">
                  <input
                    type="text"
                    name="renter"
                    value={renterDetails.renter}
                    onChange={handleRenterChange}
                    className="w-full border border-gray-300 rounded px-3 py-2"
                  />
                </td>
              </tr>
              <tr className="bg-gray-50">
                <th className="p-2 text-left bg-gray-100 w-[450px] text-red-600">
                  Email
                </th>
                <td className="p-2">
                  <input
                    type="email"
                    name="email"
                    value={renterDetails.email}
                    onChange={handleRenterChange}
                    className="w-full border border-gray-300 rounded px-3 py-2"
                  />
                </td>
              </tr>
            </tbody>
          </table>

          {/* Apartment Table */}
          <table className="w-full table-auto mb-6">
            <tbody>
              <tr>
                <th className="p-2 text-left bg-red-600 text-gray-100 w-[450px]">
                  Apartment
                </th>
                <td className="p-2">
                  <input
                    type="text"
                    name="apartment"
                    value={apartmentDetails.apartment}
                    onChange={handleApartmentChange}
                    className="w-full border border-gray-300 rounded px-3 py-2"
                  />
                </td>
              </tr>
              <tr className="bg-gray-50">
                <th className="p-2 text-left bg-gray-100 w-[450px] text-red-600">
                  Location
                </th>
                <td className="p-2">
                  <input
                    type="text"
                    name="model"
                    value={apartmentDetails.roomNum}
                    onChange={handleApartmentChange}
                    className="w-full border border-gray-300 rounded px-3 py-2"
                  />
                </td>
              </tr>
              <tr>
                <th className="p-2 text-left bg-red-600 w-[450px] text-gray-100">
                  Price
                </th>
                <td className="p-2">
                  <input
                    type="text"
                    name="price"
                    value={apartmentDetails.price}
                    onChange={handleApartmentChange}
                    className="w-full border border-gray-300 rounded px-3 py-2"
                  />
                </td>
              </tr>
            </tbody>
          </table>
          <div className="flex justify-center items-center ">
            <button className="bg-red-600 text-white rounded-lg px-4 py-2 hover:bg-gray-600 ">
              Save
            </button>
          </div>
        </section>
      </div>
    </div>
  );
};
