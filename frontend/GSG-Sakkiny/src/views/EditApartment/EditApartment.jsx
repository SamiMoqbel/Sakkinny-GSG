import React, { useRef, useEffect, useState } from "react";
import { Footer } from "../Footer";
import { Navbar } from "../Navbar";
import Select from "react-select";
import { LOCATIONS } from "../../data/DropdownInputs";
import { useParams } from "react-router-dom";
import axios from "../../api/axios";

export const EditApartment = () => {
  const { apartmentId } = useParams();
  const [formData, setFormData] = useState({});
  const imageFileRef = useRef(null); // store image in a ref to avoid triggering a re-render

  useEffect(() => {
    try {
      const fetchApartment = async () => {
        const response = await axios.get(
          `/Apartment/GetApartmentDetailsById/${apartmentId}`
        );
        const apartment = response.data;
        setFormData(apartment);
      };
      fetchApartment();
    } catch (error) {
      console.error("Error fetching apartment:", error);
    }
  }, []);

  const handleChange = (e) => {
    const { name, value } = e.target;
    setFormData({ ...formData, [name]: value });
  };

  const handleFileChange = (e) => {
    setFormData({ ...formData, image: e.target.files[0] });
    console.log(e.target.files[0]);
  };

  const handleSelectChange = (selected) => {
    setFormData({ ...formData, location: selected.value });
  };

  useEffect(() => {
    if (
      formData.base64Images &&
      formData.base64Images.length > 0 &&
      !imageFileRef.current
    ) {
      try {
        const dataUrl = `data:image/png;base64,${formData.base64Images[0]}`;
        const fetchImage = async () => {
          const response = await fetch(dataUrl);
          const blobImage = await response.blob();
          const file = new File([blobImage], "image.png", {
            type: "image/png",
          });
          imageFileRef.current = file; // store the file in the ref
          setFormData({ ...formData, image: file });
          console.log(file);
        };
        fetchImage();
      } catch (error) {
        console.error("Error loading Image:", error);
      }
    }
  }, [formData.base64Images]);

  const handleSubmit = (e) => {
    e.preventDefault();
    console.log(formData);

    try {
      const imageData = new FormData();
      if (formData.image) {
        imageData.append("Images", formData.image);
      }
      const images = [imageData];
      const putObject = {
        title: formData.title,
        subTitle: formData.subTitle,
        location: formData.location,
        roomsNumber: formData.roomsNumber,
        roomsAvailable: formData.roomsAvailable,
        price: formData.price,
        Images: images,
      };

      const response = axios.put(
        `Apartment/UpdateApartment/${apartmentId}`,
        JSON.stringify(putObject)
      );
      console.log(response.data);
    } catch (error) {
      console.error("Error submitting form:", error);
    }
  };

  return (
    <div>
      <Navbar />
      <div className="flex justify-between items-center w-full max-w-4xl mx-auto p-8 bg-gray-100 rounded-lg shadow-md mt-5 mb-10">
        <div className="flex-1 mr-8">
          <h1 className="w-full ml-[180px] text-4xl mb-10 text-center uppercase tracking-widest font-bold text-[#dad9d9] bg-gradient-to-r from-[#f36767] to-[#f71e1e] shadow-[2px_4px_6px_rgba(0,0,0,0.2)] animate-fadeInUp">
            Edit Apartment
          </h1>
          <form onSubmit={handleSubmit} className="flex flex-col">
            <input
              type="text"
              name="title"
              value={formData.title}
              onChange={handleChange}
              className="p-3 mb-3 border border-gray-300 rounded-md focus:outline-none focus:ring-2 focus:ring-red-500"
            />
            <input
              type="text"
              name="subTitle"
              value={formData.subTitle}
              onChange={handleChange}
              className="p-3 mb-3 border border-gray-300 rounded-md focus:outline-none focus:ring-2 focus:ring-red-500"
            />
            <Select
              name="location"
              options={LOCATIONS}
              value={LOCATIONS.find(
                (option) => option.value === formData.location
              )}
              onChange={handleSelectChange}
              className="p-3 mb-3 border border-gray-300 rounded-md focus:outline-none focus:ring-2 focus:ring-red-500"
            />
            <input
              type="number"
              name="roomsNumber"
              placeholder="Total Rooms Number"
              value={formData.roomsNumber}
              onChange={handleChange}
              className="p-3 mb-3 border border-gray-300 rounded-md focus:outline-none focus:ring-2 focus:ring-red-500"
            />

            <input
              type="number"
              name="roomsAvailable"
              placeholder="Available Rooms Number"
              value={formData.roomsAvailable}
              onChange={handleChange}
              className="p-3 mb-3 border border-gray-300 rounded-md focus:outline-none focus:ring-2 focus:ring-red-500"
            />

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
