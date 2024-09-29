import Select from "react-select";
import LOCATIONS from "../../data/LOCATIONS";
import axios from "../../api/axios";
import { useEffect, useState } from "react";

export const SearchBar = () => {
  const [apartmentOptions, setApartmentOptions] = useState([]);

  const roomsOptions = [
    { value: "1", label: "1" },
    { value: "2", label: "2" },
    { value: "3", label: "3" },
    { value: "4", label: "4" },
    { value: "5", label: "5" },
    { value: "6", label: "6" },
  ];

  const locationsOptions = LOCATIONS.map((location) => ({
    value: location,
    label: location,
  }));

  useEffect(() => {
    const fetchNames = async () => {
      try {
        const response = await axios.get(
          "/Apartment/GetAllApartmentNames/names"
        );
        const names = [...new Set(response.data)];
        setApartmentOptions(
          names.map((name) => ({ value: name, label: name }))
        );
      } catch (error) {
        console.error(error);
      }
    };
    fetchNames();
  }, []);

  return (
    <div className="mt-6 rounded-2xl border-2 w-2/3 p-8 bg-red-600 flex items-center justify-center gap-3">
      <Select
        defaultValue={apartmentOptions[0]}
        name="apartments"
        options={apartmentOptions}
        isSearchable
        isClearable
        className="w-1/3 "
      />
      <Select
        isMulti
        defaultValue={[locationsOptions[0]]}
        name="locations"
        options={locationsOptions}
        className="w-1/5"
      />
      <Select
        isMulti
        defaultValue={[roomsOptions[0]]}
        name="rooms"
        options={roomsOptions}
        isSearchable={false}
        className="w-1/5"
      />

      <button className="bg-black text-white font-semibold rounded-md py-2 px-10">
        Search
      </button>
    </div>
  );
};
