import { useEffect, useState } from "react";
import { useInfiniteQuery } from "@tanstack/react-query";
import { useInView } from "react-intersection-observer";
import { getApartments } from "../../api/getApartments";
import { ApartmentCard, SearchBar } from "../../components";
import { Footer } from "../Footer";
import { Navbar } from "../Navbar";

export const Home = () => {



  const {
    data,
    error,
    fetchNextPage,
    hasNextPage,
    status,
  } = useInfiniteQuery({
    queryKey: ["apartments"],
    queryFn: getApartments,
    initialPageParam: 1,
    getNextPageParam: (lastPage, allPages, lastPageParam) => {
      if (lastPage.length === 0) {
        return undefined;
      }
      return lastPageParam + 1;
    },
  });

  const { ref, inView } = useInView();

  useEffect(() => {
    if (inView && hasNextPage) {
      fetchNextPage();
    }
  }, [inView, fetchNextPage, hasNextPage]);

  const [authenticated, setAuthenticated] = useState(true);

  return (
    <div className="w-full h-full flex flex-col">
      <Navbar authenticated={authenticated} />
      <main className=" flex-1 flex flex-col justify-center items-center">
        <SearchBar />
        <section className="flex-1 p-9 flex flex-wrap gap-4 justify-center ">
          {status === "pending" ? (
            <p>Loading...</p>
          ) : status === "error" ? (
            <p>Error: {error.message}</p>
          ) : (
            <>
              {data?.pages.map((page) =>
                page.apartments.map((apartment) => (
                  <ApartmentCard
                    key={apartment.id}
                    id={apartment.id}
                    title={apartment.title}
                    subtitle={apartment.subTitle}
                  />
                ))
              )}
            </>
          )}
        </section>
        <div ref={ref}></div>
      </main>
      <Footer />
    </div>
  );
};
