import { Link } from "react-router-dom";
import { Footer } from "../Footer";
import { Navbar } from "../Navbar";
import { useEffect, useState } from "react";
import axios from "../../api/axios";

export const Apartments= () =>{
    const [apartmentsData, setApartmentsData] = useState([]);

    useEffect(() => {
        const fetchApartments = async () => {
          try {
            const response = await axios.get("/Apartment/GetAllApartments");
            setApartmentsData(response.data.apartments);
            
          } catch (error) {
            console.error(error);
          }
        };

        fetchApartments();
    }, []);
     
    return (
    <div className="flex min-h-screen font-sans">
 
    <div className="flex-grow bg-gray-100 p-6">
    <Navbar />
      <div className="bg-white h-screen p-6 rounded-lg shadow-md mt-[15] mb-[30px]">
        <h3 className="text-xl font-semibold mb-4">Apartments</h3>
        <table className="min-w-full border-collapse w-auto">
          <thead>
            <tr className="border-b">
              <th className="text-left py-2">Title</th>
              <th className="text-left py-2">Location</th>
              <th className="text-left py-2">Rooms Num</th>
              <th className="text-left py-2">Rooms Available</th>
              <th className="text-left py-2">Price</th>
              <th className="text-left py-2">Action</th>
            </tr>
          </thead>
          <tbody>
            {apartmentsData.map((apartments, index) => (
              <tr key={index} className="border-b">
                <td className="py-2"><Link className="text-blue-400 hover:underline" to={`/apartments/${apartments.id}`}>{apartments.title}</Link></td>
                <td className="py-2">{apartments.location}</td>
                <td className="py-2">{apartments.roomsNumber}</td>
                <td className="py-2">{apartments.roomsAvailable}</td>
                <td className="py-2">{apartments.price}</td>
                <td className="py-2">
                  <button className="bg-blue-500 text-white px-4 py-2 rounded-md mr-2">Edit</button>
                  <button className="bg-red-500 text-white px-4 py-2 rounded-md">Delete</button>
                </td>
              </tr>
            ))}
            
          </tbody>
        </table>
        <div className="mt-8  flex justify-center">
            <button className="bg-purple-600 text-white py-4 px-10 rounded-lg text-lg">Add Apartments</button>
         </div>
      </div>

     

     <Footer />
    </div>
   
  </div>
  );
}


