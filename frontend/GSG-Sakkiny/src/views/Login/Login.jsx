import { useState } from "react";
import { SocialIcon } from "react-social-icons";
import { CustomInput } from "../../components";
import infoImage from "../../assets/info-image.png";
import { Link } from "react-router-dom";
import axios from "../../api/axios";


export const Login = () => {
  const [userInput, setUserInput] = useState({ email: "", password: "" });

  const handleChange = (e) => {
    setUserInput({ ...userInput, [e.target.name]: e.target.value });
  };

  const handleSubmit = async (e) => {
    e.preventDefault();

    try {
      const response = await axios.post(
        "/Auth/login",
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
        <div className="flex-1 p-[40px] flex flex-col justify-around">
          <h2 className="text-4xl text-gray-600 self-center font-bold">
            Welcome To
            <span className="text-red-600"> Sakkinny</span>{" "}
          </h2>
          <p className="self-center text-lg text-gray-400 font-bold">
            Log in to get in the moment updates on the things that interest you.
          </p>

          <form
            onSubmit={handleSubmit}
            className="flex flex-col h-1/2 justify-around"
          >
            <CustomInput
              type="text"
              placeholder="Email"
              value={userInput.email}
              name="email"
              handleChange={handleChange}
            />

            <CustomInput
              type="password"
              placeholder="Password"
              value={userInput.password}
              name="password"
              handleChange={handleChange}
            />

            <button
              type="submit"
              className=" p-4 text-sm mt-4 w-full border-0 bg-red-600  rounded-[20px]"
            >
              <span className="font-bold text-white text-base">Sign In</span>
            </button>
          </form>

          <p className="self-center">
            Don’t have an account? <Link className="text-red-600 font-bold" to="/signup">Sign Up</Link>
          </p>

          <div
            id="social-login"
            className=" flex flex-col justify-center items-center "
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

        {/* Right side (Informational Section) */}
        <div className="flex-1 flex justify-center items-center">
          <img className="w-full h-full" src={infoImage} alt="Signup" />
        </div>
      </div>
    </div>
  );
};
