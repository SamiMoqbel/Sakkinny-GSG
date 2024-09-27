import { FormInput, UserCard } from "../../components";
import { InputGroup } from "../../components/InputGroup";
import { Footer } from "../Footer";
import { Navbar } from "../Navbar";

export const AccountSettings = () => {
  return (
    <>
      <Navbar />
      <main className="bg-white h-screen border-2 flex flex-col items-center">
        <div className="w-2/3 h-4/5 mt-20 flex flex-col">
          <h3 className="text-3xl font-semibold mb-4 self-start">
            Account Settings
          </h3>
          <div className="border-2 shadow-lg rounded-lg flex-1 flex justify-between">
            <div className="flex-1 mt-20 flex flex-col items-center gap-2">
              <InputGroup
                src="name"
                label="Display Name"
                placeholder="John Doe"
                type="text"
                hasHelper={false}
              />
              <InputGroup
                src="email"
                label="Email Address"
                placeholder="johndoe@email.com"
                type="email"
                hasHelper={true}
              />
              <InputGroup
                src="password"
                label="Password"
                placeholder="************"
                type="password"
                hasHelper={true}
              />
              <div className="mt-12 flex justify-center items-center gap-20">
                <button className="hover:opacity-75 active:scale-90 bg-blue-500 border-2 text-white font-bold border-black px-12 py-2 rounded-md">
                  Save
                </button>
                <button className="hover:opacity-75 active:scale-90 bg-gray-200 border-2 border-black px-12 py-2 font-bold rounded-md">
                  Cancel
                </button>
              </div>
            </div>
            <div className="flex flex-col w-1/3 items-center mt-24 gap-8">
              <img
                className="w-40 rounded-full"
                src="https://picsum.photos/100"
                alt="profile picture"
              />
              <button className=" hover:opacity-75 active:scale-90 border-2 border-black rounded-md bg-blue-500 font-bold text-white p-3 ">
                Change Picture
              </button>
            </div>
          </div>
        </div>
      </main>
      <Footer />
    </>
  );
};
