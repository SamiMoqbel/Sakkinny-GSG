import { useState } from "react";
import { SocialIcon } from "react-social-icons";
import { CustomInput } from "../../components";
import infoImage from "../../assets/info-image.png";
import { Link } from "react-router-dom";
import axios from "../../api/axios";

export const Signup = () => {
  const [userInput, setUserInput] = useState({
    fullName: "",
    email: "",
    role: "Client",
    password: "",
    coPassword: "",
  });

  const handleChange = (e) => {
    setUserInput({ ...userInput, [e.target.name]: e.target.value });
  };

  const handleSubmit = async (e) => {
    e.preventDefault();

    try {
      const response = await axios.post(
        "/Auth/register",
        JSON.stringify(userInput),
        {
          headers: {
            "Content-Type": "application/json",
          },
          withCredentials: true,
        }
      );
      console.log(response.data);
    } catch (error) {
      console.log(error);
    }

  };

  return (
    <div className="h-full w-full flex items-center justify-center bg-[#f8f9fa]">
      <div className="flex bg-white rounded-lg overflow-hidden w-2/3 border-2">
        <div className="flex-1 flex justify-center items-center">
          <img className="w-full h-full" src={infoImage} alt="Signup" />
        </div>
        <div className="flex-1 flex flex-col p-[40px]">
          <h2>
            Create an account on
            <span className="font-bold text-red-600"> Sakkinny</span>{" "}
          </h2>
          <form onSubmit={handleSubmit}>
            <CustomInput
              type="text"
              placeholder="Email"
              value={userInput.email}
              name="email"
              handleChange={handleChange}
            />

            <CustomInput
              type="text"
              placeholder="Full Name"
              value={userInput.fullName}
              name="fullName"
              handleChange={handleChange}
            />

            <CustomInput
              type="password"
              placeholder="Password"
              value={userInput.password}
              name="password"
              handleChange={handleChange}
            />

            <CustomInput
              type="password"
              placeholder="Confirm Password"
              value={userInput.coPassword}
              name="coPassword"
              handleChange={handleChange}
            />

            <button
              type="submit"
              className=" p-4 text-sm w-full border-0 bg-red-600 mt-5 rounded-[20px]"
            >
              <span className="font-bold text-white text-base">Sign Up</span>
            </button>
          </form>

          <p className="self-center mt-4">
            Already have an account?{" "}
            <Link className="text-red-600 font-bold" to="/login">
              Sign In
            </Link>
          </p>

          <div
            id="social-login"
            className="mt-4 flex flex-col justify-center items-center "
          >
            <p>or continue with </p>
            <div
              id="social-links"
              className="flex w-3/4 justify-around items-center mt-4"
            >
              <SocialIcon url="https://facebook.com/" />
              <SocialIcon url="http://accounts.google.com" />
              <SocialIcon url="https://github.com/" />
            </div>
          </div>
        </div>
      </div>
    </div>
  );
};
