import Image from "next/image";
import Link from "next/link";
import { Della_Respira } from "next/font/google";
const della = Della_Respira({ subsets: ["latin"], weight: "400" });
const AboutFill = () => {
  return (
    <div className="grid lg:grid-cols-2 bg-orange-100 py-24">
      <div className="flex flex-col  mx-12">
        <h1 className={`text-6xl mx-12 `}>About the Innkeeper</h1>
        <p className="my-8 text-2xl mx-12">
          Greetings! I'm Sarah, your host at the cozy Dahlia Inn, nestled in the heart of the breathtaking Blue Ridge Mountains. I've always been drawn to the tranquility and natural beauty of this place and I'm thrilled to share it with you.  Whether you're here to hike, explore, or simply relax and recharge, I'm committed to making your stay as comfortable and enjoyable as possible. I can't wait to welcome you to this special corner of the world!
        </p>
        <div className="mx-12">
        <a href="/book">
          {" "}
          <div className={`${della.className}`}>
          <button className="text-4xl text-sky-900 my-2 bg-sky-300 rounded-2xl px-12 py-2 text-neutral-800">
            Book your Stay
          </button>
          </div>
        </a>{" "}
        </div>
      </div>
      <div >
        <div className="flex relative">
          <Image src="/bmliz.png" alt="Elizabeth" width={800} height={500} className="rounded-3xl" />

        </div>
      </div>
    </div>
  );
};
export default AboutFill;
