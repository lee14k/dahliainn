import React, { useEffect, useState } from "react";
import { useRouter } from "next/router";
import { useBooking } from "../context/BookingContext";
import { supabase } from '../utils/supabase'; // Adjust the import path as needed
import { ClipLoader } from "react-spinners"; // Import the ClipLoader from react-spinners

const PayForm = ({ bookingId, onPaymentSuccess }) => {
  const router = useRouter();
  const { selectedRoom, setPaymentId, selectedDates } = useBooking();
  const [rate, setRate] = useState(null);
  const [paymentLink, setPaymentLink] = useState("");
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    if (!selectedRoom || !selectedRoom.id) {
      console.error("selectedRoom is undefined or has no id");
      setLoading(false); // Stop loading if there's an error
      return;
    }

    console.log("Selected Room ID:", selectedRoom.id);

    const fetchRate = async () => {
      const { data, error } = await supabase
        .from('rooms')
        .select('rate')
        .eq('id', selectedRoom.id)
        .single();

      if (error) {
        console.error('Error fetching room rate:', error);
        setLoading(false); // Stop loading if there's an error
      } else {
        setRate(data.rate);
        setLoading(false); // Stop loading when the rate is fetched
      }
    };

    fetchRate();
  }, [selectedRoom]);

  const handleCreatePaymentLink = async () => {
    if (!bookingId || !rate || !selectedDates || selectedDates.length !== 2) {
      console.error("Missing required data for creating payment link");
      return;
    }

    try {
      const numberOfDays = Math.max(1, Math.round((new Date(selectedDates[1]) - new Date(selectedDates[0])) / (1000 * 60 * 60 * 24)));
      const totalAmount = rate * numberOfDays * 100; // Convert to cents

      const response = await fetch("/api/create-payment-link", {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify({
          amount: totalAmount,
          bookingId,
          roomId: selectedRoom.id,
          numberOfDays,
          metadata: {
            bookingId,
            roomId: selectedRoom.id
          }
        }),
      });

      if (response.ok) {
        const result = await response.json();
        setPaymentLink(result.paymentLink);
      } else {
        console.error("Failed to create payment link:", await response.text());
      }
    } catch (error) {
      console.error("Error creating payment link:", error);
    }
  };

  useEffect(() => {
    if (rate !== null) {
      handleCreatePaymentLink();
    }
  }, [rate]);

  if (loading) {
    return (
      <div className="flex justify-center items-center min-h-screen">
        <ClipLoader size={250} color={"#123abc"} loading={loading} />
      </div>
    );
  }

  if (paymentLink) {
    window.location.href = paymentLink;
    return <div>Redirecting to payment...</div>;
  }

  return (
    <div>
    </div>
  );
};

export default PayForm;
