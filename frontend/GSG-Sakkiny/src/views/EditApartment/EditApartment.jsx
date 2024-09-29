import React, { useState } from "react";
import { Footer } from "../Footer";
import { Navbar } from "../Navbar";
// import { useParams } from 'react-router-dom';

export const EditApartment= () =>{
  // const {title } = useParams(); 
  
  const [formData, setFormData] = useState({
    title: "",
    subtitle: "",
    // location: "",
    totalRooms: "",
    price: "",
    image: null,
  });
  const [selectedOption, setSelectedOption] = useState("");

  const handleChange = (e) => {
    const { name, value } = e.target;
    setFormData({ ...formData, [name]: value });
    setSelectedOption(e.target.value);
  };

  const handleFileChange = (e) => {
    setFormData({ ...formData, image: e.target.files[0] });
  };

  const handleSubmit = (e) => {
    e.preventDefault();
    console.log("Form submitted", formData);
  };

  return (
     <div><Navbar />
    <div className="flex justify-between items-center w-full max-w-4xl mx-auto p-8 bg-gray-100 rounded-lg shadow-md mt-5 mb-10">
            <div className="flex-1 mr-8">
        <h1 class="w-full ml-[180px] text-4xl mb-10 text-center uppercase tracking-widest font-bold text-[#dad9d9] bg-gradient-to-r from-[#f36767] to-[#f71e1e] shadow-[2px_4px_6px_rgba(0,0,0,0.2)] animate-fadeInUp">
          Edit Apartment
        </h1>
        <form onSubmit={handleSubmit} className="flex flex-col">
          <input
            type="text"
            name="title"
            placeholder="Title"
            value={formData.title}
            onChange={handleChange}
            className="p-3 mb-3 border border-gray-300 rounded-md focus:outline-none focus:ring-2 focus:ring-red-500"
          />
          <input
            type="text"
            name="subtitle"
            placeholder="Sub Title"
            value={formData.subtitle}
            onChange={handleChange}
            className="p-3 mb-3 border border-gray-300 rounded-md focus:outline-none focus:ring-2 focus:ring-red-500"
          />
          {/* <input
            type="text"
            name="location"
            placeholder="Location"
            value={formData.location}
            onChange={handleChange}
            className="p-3 mb-3 border border-gray-300 rounded-md focus:outline-none focus:ring-2 focus:ring-red-500"
          /> */}
          <input
            type="number"
            name="totalRooms"
            placeholder="Total Rooms Num"
            value={formData.totalRooms}
            onChange={handleChange}
            className="p-3 mb-3 border border-gray-300 rounded-md focus:outline-none focus:ring-2 focus:ring-red-500"
          />
          <select
            value={selectedOption}
            onChange={handleChange}
            className="p-3 mb-3 border border-gray-300 rounded-md focus:outline-none focus:ring-2 focus:ring-red-500"
          >
            <option value="" disabled>
              Apartment Available!!
            </option>
            <option value="available">Available</option>
            <option value="not available">Not Available</option>
          </select>

          <input
            type="file"
            name="image"
            onChange={handleFileChange}
            className="p-3 mb-3 border border-gray-300 rounded-md focus:outline-none focus:ring-2 focus:ring-red-500"
          />

          <input
            type="number"
            name="price"
            placeholder="Price"
            value={formData.price}
            onChange={handleChange}
            className="p-3 mb-3 border border-gray-300 rounded-md focus:outline-none focus:ring-2 focus:ring-red-500"
          />
          <button
            type="submit"
            className="bg-red-500 text-white p-3 rounded-md font-medium hover:bg-red-600 transition-colors"
          >
            Edit Apartment 😊
          </button>
        </form>
      </div>
      <div className="flex-1 flex justify-center items-center">
        <img
          src="https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQxQJkYcrdP93CozvznZQexLNMnHJITlciV1yMUuVyimNbFOda1lkDwYqZpJxFIZHUCvOE&usqp=CAU"
          alt="edit"
          className="p-3 max-w-full h-auto rounded-lg shadow-md"
        />
      </div>
    </div>
    <Footer />
      </div>
  );
};


