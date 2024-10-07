import { useState, useEffect } from "react";
import { Navbar } from "../Navbar";
import { Footer } from "../Footer";
import { UserCard } from "../../components";
import { Carousel } from "flowbite-react";
import { useParams } from "react-router-dom";
import axios from "../../api/axios";

export const ApartmentDetails = () => {
  const [apartmentDetails, setApartmentDetails] = useState({});
  const [ownerDetails, setOwnerDetails] = useState({});

  const { apartmentId } = useParams();

  useEffect(() => {
    try {
      const fetchApartment = async () => {
        const response = await axios.get(
          `/Apartment/GetApartmentDetailsById/${apartmentId}`
        );
        const apartment = response.data;
        console.log(apartment);
        setApartmentDetails(apartment);
      };

      fetchApartment();
    } catch (error) {
      console.error("Error fetching apartment:", error);
    }
  }, []);

  useEffect(() => {
    const fetchOwner = async () => {
      const response = await axios.get(
        `/Auth/getUserById/${apartmentDetails.ownerId}`
      );
      const owner = response.data;
      setOwnerDetails(owner);
    };
    fetchOwner();
  }, [apartmentDetails]);

  return (
    <>
      <Navbar />
      <main>
        <div className="p-24 h-full flex justify-between ">
          <div className="flex flex-col flex-1 ">
            <div className="h-[600px]">
              <Carousel slideInterval={5000}>
                {apartmentDetails.base64Images &&
                  apartmentDetails.base64Images.map((image, index) => (
                    <img
                      key={index}
                      className="w-full h-full object-cover"
                      src={`data:image/png;base64,${image}`}
                      alt="..."
                    />
                  ))}
              </Carousel>
            </div>
            <div className="mt-6">
              <div className="border-2 rounded-t-md border-t-gray-300 ">
                <div className="flex justify-between py-4 px-10">
                  <h1 className="text-3xl font-bold">
                    {apartmentDetails.title}
                  </h1>
                  <h1 className="text-3xl text-green-500 font-bold">
                    ${apartmentDetails.price}
                  </h1>
                </div>
              </div>
              <div className="border-2 border-t-0 rounded-b-md border-gray-300 px-10 py-4">
                <h2 className="font-bold text-2xl text-red-600">Details</h2>
                <p className="mt-4">{apartmentDetails.subTitle}</p>
                <ul className="mt-6 flex flex-col gap-8">
                  <li className="border-b-2 pb-4">
                    <ul className="flex justify-between px-6">
                      <li className="font-bold text-xl">Location</li>
                      <li>{apartmentDetails.location}</li>
                    </ul>
                  </li>
                  <li className="border-b-2 pb-4">
                    <ul className="flex justify-between px-6">
                      <li className="font-bold text-xl">Rooms Number</li>
                      <li>{apartmentDetails.roomsNumber}</li>
                    </ul>
                  </li>

                  <li className="border-b-2 pb-4">
                    <ul className="flex justify-between px-6">
                      <li className="font-bold text-xl">Available Rooms</li>
                      <li>{apartmentDetails.roomsAvailable}</li>
                    </ul>
                  </li>
                </ul>
              </div>
            </div>
          </div>
          <div className="ml-20">
            <UserCard
              ownerDetails={ownerDetails}
              apartmentDetails={apartmentDetails}
              apartmentId={apartmentId}
            />
          </div>
        </div>
      </main>
      <Footer />
    </>
  );
};
