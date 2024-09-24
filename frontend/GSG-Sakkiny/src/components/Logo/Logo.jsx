import infoImage from "../../assets/info-image.png";

export const Logo = () => {
  return (
    <div className="flex items-center justify-center">
      <img src={infoImage} alt="Sakkinny" className="h-10 rounded-full" />
      <h1 className="text-3xl font-bold ml-4">Sakkinny</h1>
    </div>
  );
};
