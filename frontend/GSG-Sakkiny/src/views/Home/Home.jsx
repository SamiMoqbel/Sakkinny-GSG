import { ApartmentCard, SearchBar } from "../../components";
import { useState } from "react";
import { Footer } from "../Footer";
import { Navbar } from "../Navbar";

export const Home = () => {

  const [authenticated, setAuthenticated] = useState(true);

  return (
    <div className="w-full h-full flex flex-col">
      <Navbar authenticated={authenticated} />
      <main className="flex-1 flex flex-col justify-center items-center">
        <SearchBar />
        <section className="flex-1 p-9 flex flex-wrap gap-4 justify-center ">
          <ApartmentCard />
          <ApartmentCard />
          <ApartmentCard />
          <ApartmentCard />
          <ApartmentCard />
          <ApartmentCard />
          <ApartmentCard />
          <ApartmentCard />
          <ApartmentCard />
          <ApartmentCard />
          <ApartmentCard />
          <ApartmentCard />
          <ApartmentCard />
          <ApartmentCard />
          <ApartmentCard />
          <ApartmentCard />
          <ApartmentCard />
          <ApartmentCard />
          <ApartmentCard />
          <ApartmentCard />
          <ApartmentCard />
          <ApartmentCard />
          <ApartmentCard />
          <ApartmentCard />
          <ApartmentCard />
          <ApartmentCard />
          <ApartmentCard />
          <ApartmentCard />
          <ApartmentCard />
          <ApartmentCard />
          <ApartmentCard />
          <ApartmentCard />
        </section>
      </main>
      <Footer />
    </div>
  );
};
