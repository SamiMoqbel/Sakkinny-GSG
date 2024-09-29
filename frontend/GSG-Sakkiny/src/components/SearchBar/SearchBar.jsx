import Select from "react-select";
import LOCATIONS from "../../data/LOCATIONS";

export const SearchBar = () => {
  const options = [
    { value: "chocolate", label: "Chocolate" },
    { value: "strawberry", label: "Strawberry" },
    { value: "vanilla", label: "Vanilla" },
  ];

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

  return (
    <div className="mt-6 rounded-2xl border-2 w-2/3 p-8 bg-red-600 flex items-center justify-center gap-3">
      <Select
        defaultValue={options[0]}
        name="apartments"
        options={options}
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
