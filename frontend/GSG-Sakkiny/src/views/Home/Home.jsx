import { Link } from "react-router-dom";
import { Dropdown } from "flowbite-react";
import Avatar from "react-avatar";
import infoImage from "../../assets/info-image.png";
import { ApartmentCard } from "../../components";

export const Home = () => {
  return (
    <div className="w-full h-full flex flex-col">
      <nav className=" min-h-24 flex items-center justify-between px-10">
        <div className="flex items-center justify-center">
          <img src={infoImage} alt="Sakkinny" className="h-10 rounded-full" />
          <h1 className="text-3xl font-bold ml-4">Sakkinny</h1>
        </div>
        <Dropdown
          label={
            <Avatar
              src="http://www.gravatar.com/avatar/a16a38cdfe8b2cbd38e8a56ab93238d3"
              size="50"
              round={true}
            />
          }
          arrowIcon={false}
          inline
          className="bg-red-600 text-white rounded-md"
        >
          <Dropdown.Header>
            <span className="block text-sm">Bonnie Green</span>
            <span className="block truncate text-sm font-medium">
              name@flowbite.com
            </span>
          </Dropdown.Header>
          <Dropdown.Item>Dashboard</Dropdown.Item>
          <Dropdown.Item>Settings</Dropdown.Item>
          <Dropdown.Item>Earnings</Dropdown.Item>
          <Dropdown.Divider />
          <Dropdown.Item>
            <Link to="/login">Sign out</Link>
          </Dropdown.Item>
        </Dropdown>
      </nav>
      <main className="flex-1 flex ">
        <aside className="bg-blue-300 md:min-w-56">Aside</aside>
        <section className="flex-1 p-9 flex flex-wrap gap-4 ">
          <ApartmentCard />
          <ApartmentCard />
          <ApartmentCard />
          <ApartmentCard />
          <ApartmentCard />
          <ApartmentCard />
          <ApartmentCard />
          <ApartmentCard />
          <ApartmentCard />
          <ApartmentCard />
          <ApartmentCard />
          <ApartmentCard />
          <ApartmentCard />
          <ApartmentCard />
          <ApartmentCard />
          <ApartmentCard />
          <ApartmentCard />
          <ApartmentCard />
          <ApartmentCard />
          <ApartmentCard />
          <ApartmentCard />
          <ApartmentCard />
          <ApartmentCard />
          <ApartmentCard />
          <ApartmentCard />
          <ApartmentCard />
          <ApartmentCard />
          <ApartmentCard />
        </section>
      </main>
      <footer className="bg-gray-600 min-h-56">Footer</footer>
    </div>
  );
};
