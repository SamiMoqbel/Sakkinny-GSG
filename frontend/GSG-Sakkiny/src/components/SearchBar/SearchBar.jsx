import Select from "react-select";
import { LOCATIONS, ROOMS_COUNT } from "../../data/DropdownInputs";
import axios from "../../api/axios";
import { useEffect, useState } from "react";

export const SearchBar = () => {
  const DEFAULT_LOCATION = LOCATIONS[0];
  const DEFAULT_ROOMS_COUNT = ROOMS_COUNT[0];

  const [apartmentNames, setApartmentNames] = useState([]);

  const [filters, setFilters] = useState({
    apartmentName: "",
    locations: [],
    rooms: [],
  });

  useEffect(() => {
    const fetchNames = async () => {
      try {
        const response = await axios.get(
          "/Apartment/GetAllApartmentNames/names"
        );
        const names = [...new Set(response.data)];
        setApartmentNames(names.map((name) => ({ value: name, label: name })));
      } catch (error) {
        console.error(error);
      }
    };
    fetchNames();
  }, []);

  const handleChange = (selected, { name }) => {
    if (name === "apartmentName") {
      return setFilters((prevState) => ({
        ...prevState,
        [name]: selected.value,
      }));
    }
    setFilters((prevState) => ({
      ...prevState,
      [name]: selected.map((item) => item.value),
    }));
  };

  const handleSearch = () => {
    
  };

  return (
    <div className="mt-6 rounded-2xl border-2 w-2/3 p-8 bg-red-600 flex items-center justify-center gap-3">
      <Select
        name="apartmentName"
        options={apartmentNames}
        isSearchable
        isClearable
        onChange={handleChange}
        className="w-1/3 "
      />
      <Select
        isMulti
        name="locations"
        options={LOCATIONS}
        onChange={handleChange}
        className="w-1/5"
      />

      <Select
        isMulti
        name="rooms"
        options={ROOMS_COUNT}
        isSearchable={false}
        onChange={handleChange}
        className="w-1/5"
      />

      <button className="bg-black text-white font-semibold rounded-md py-2 px-10">
        Search
      </button>
    </div>
  );
};
