import { Della_Respira } from "next/font/google";
import FacebookTwoToneIcon from '@mui/icons-material/FacebookTwoTone';
import Link from "next/link";
import Image from "next/image";
const della = Della_Respira({ subsets: ["latin"], weight: "400" });
const Footer = ({isMobile}) => {
  return (
    <div className="footerwrapper bg-orange-50 flex  ">
      {isMobile ? (
        <div>
          {/* Mobile Footer Content */}
          <p>Mobile Footer</p>
        </div>
      ) : (
        <div className="lg:flex flex-col container ml-auto text-center ">
          <div className=" text-center lg:text-right">
            <h1 className={`${della.className} text-6xl`}>Blue Mystique Inn</h1>
            <p className="text-3xl">230 Arbutus St, Manistique, MI 49854</p>
            <div className="flex flex-col text-xl">
              <span>(906) 221-5371</span>
              <span>bluemystiquerentals@gmail.com</span>
              <Link href='https://www.facebook.com/bluemystiquerentals'>
                <FacebookTwoToneIcon fontSize="large"/>
              </Link>
            </div>
          </div>
        </div>
      )}
    </div>
  );
};
export default Footer;
