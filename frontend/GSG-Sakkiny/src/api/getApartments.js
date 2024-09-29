import axios from "./axios";

export const getApartments = async ({ pageParam }) => {
  try {
    const response = await axios.get(
      `/Apartment/GetAllApartments?PageIndex=${pageParam}`
    );
    return response.data;
  } catch (error) {
    console.error(error);
  }
};
