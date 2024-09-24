import Select from "react-select";

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
  ];

  return (
    <div className="mt-6 rounded-sm border-2 w-2/3 p-8 bg-gray-100 flex items-center justify-center gap-3">
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
        defaultValue={[options[0]]}
        name="locations"
        options={options}
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

      <button className="bg-blue-700 text-white rounded-md py-2 px-10">Search</button>
    </div>
  );
};
