import { Card } from "flowbite-react";

export const UserCard = () => {
  return (
    <Card className="max-w-sm  px-14">
      <div className="flex flex-col items-center pb-10">
        <img
          alt="Bonnie image"
          height="96"
          src="https://picsum.photos/100"
          width="96"
          className="mb-3 rounded-full shadow-lg"
        />
        <h5 className="mb-1 text-xl font-medium text-gray-900 ">
          Bonnie Green
        </h5>
        <span className="text-sm font-semibold text-blue-600">
          LANDLORD
        </span>
        <div className="mt-4 flex space-x-3 lg:mt-6">
          <a
            href="#"
            className="inline-flex items-center rounded-lg border border-gray-300 bg-white px-4 py-2 text-center text-sm font-medium text-gray-900 hover:bg-green-500 hover:text-white focus:outline-none focus:ring-4 focus:ring-gray-200  "
          >
            Rent
          </a>
        </div>
      </div>
    </Card>
  );
};
