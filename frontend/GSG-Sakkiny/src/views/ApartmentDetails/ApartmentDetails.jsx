import { useState } from "react";
import { Navbar } from "../Navbar";
import { Footer } from "../Footer";
import { UserCard } from "../../components";
import { Carousel } from "flowbite-react";

export const ApartmentDetails = () => {
  const apartmentDetails = {
    location: "Lekki",
    title: "Samis Apartment",
    roomsNum: 3,
    roomsAvailable: 2,
    price: "1000000",
  };

  const [index, setIndex] = useState(0);

  const handleSelect = (selectedIndex) => {
    setIndex(selectedIndex);
  };

  return (
    <>
      <Navbar />
      <main>
        <div className="p-24 h-full flex justify-between ">
          <div className="flex flex-col flex-1 ">
            <div className="h-[600px]">
              <Carousel slideInterval={5000}>
                <img src="https://picsum.photos/1000" alt="..." />
                <img src="https://picsum.photos/1000" alt="..." />
                <img src="https://picsum.photos/1000" alt="..." />
                <img src="https://picsum.photos/1000" alt="..." />
                <img src="https://picsum.photos/1000" alt="..." />
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
                <p className="mt-4">
                  Lorem ipsum, dolor sit amet consectetur adipisicing elit.
                  Repellendus cupiditate dignissimos amet nisi, cum non.
                  Temporibus labore sapiente facere nobis maxime, molestias,
                  perspiciatis quam esse eum exercitationem quidem? Quia, modi.
                </p>
                <ul className="mt-6 flex flex-col gap-8">
                  <li className="border-b-2 pb-4">Item</li>
                  <li className="border-b-2 pb-4">Item</li>
                  <li className="border-b-2 pb-4">Item</li>
                  <li className="border-b-2 pb-4">Item</li>
                </ul>
              </div>
            </div>
          </div>
          <div className="ml-20">
            <UserCard />
          </div>
        </div>
      </main>
      <Footer />
    </>
  );
};