import axios from "./axios";

export const getApartments = async ({ pageParam }) => {
  try {
    const response = await axios.post(
      `/Apartment/GetAllApartments`,
      JSON.stringify({ pageIndex: pageParam }),
      {
        headers: {
          "Content-Type": "application/json",
        },
      }
    );
    return response.data;
  } catch (error) {
    console.error(error);
  }
};
