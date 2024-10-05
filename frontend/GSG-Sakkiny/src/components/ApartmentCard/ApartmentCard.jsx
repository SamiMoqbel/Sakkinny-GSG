import React from "react";
import { Link } from "react-router-dom";

export const ApartmentCard = ({id, title, subtitle}) => {
  return (
    <div className="max-w-sm bg-white border border-gray-200 rounded-lg shadow ">
      <Link to={`/apartments/${id}`}>
        <img className="rounded-t-lg" src="https://picsum.photos/500" alt="" />
      </Link>
      <div className="p-5 flex flex-col">
        <Link to={`/apartments/${id}`}>
          <h5 className="mb-2 text-2xl font-bold tracking-tight text-gray-900 ">
            {title}
          </h5>
        </Link>
        <p className="mb-3 font-normal text-gray-700 ">
          {subtitle}
        </p>
        <Link
          to={`/apartments/${id}`}
          className="self-end inline-flex items-center px-3 py-2 text-sm font-medium text-center text-white bg-black rounded-lg hover:bg-red-600  focus:ring-4 focus:outline-none focus:ring-black "
        >
          Read more
          <svg
            className="rtl:rotate-180 w-3.5 h-3.5 ms-2"
            aria-hidden="true"
            xmlns="http://www.w3.org/2000/svg"
            fill="none"
            viewBox="0 0 14 10"
          >
            <path
              stroke="currentColor"
              strokeLinecap="round"
              strokeLinejoin="round"
              strokeWidth="2"
              d="M1 5h12m0 0L9 1m4 4L9 9"
            />
          </svg>
        </Link>
      </div>
    </div>
  );
};
