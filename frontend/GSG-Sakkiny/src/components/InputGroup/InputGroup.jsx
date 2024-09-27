import { Label, TextInput } from "flowbite-react";

export const InputGroup = ({
  src,
  label,
  placeholder,
  type,
  hasHelper,
  ...rest
}) => {
  return (
    <div className="max-w-md w-full flex flex-col">
      <div className="mb-2 block">
        <Label htmlFor={src} value={label} />
      </div>
      <TextInput id={src} type={type} placeholder={placeholder} {...rest} />
      {hasHelper && (
        <button className="mt-2 text-blue-500 hover:underline self-end">
          Change
        </button>
      )}
    </div>
  );
};
