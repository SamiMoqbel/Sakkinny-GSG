import { Link } from "react-router-dom";
import { Dropdown } from "flowbite-react";
import Avatar from "react-avatar";
import { Logo } from "../../components";

export const Navbar = () => {
  return (
    <nav className="bg-gray-100 min-h-24 flex items-center justify-between px-10">
      <Logo />
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
        className="bg-gray-100 text-white rounded-md"
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
  );
};
