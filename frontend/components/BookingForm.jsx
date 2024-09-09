import React, { useState, useEffect } from "react";
import { LocalizationProvider } from "@mui/x-date-pickers/LocalizationProvider";
import { AdapterDayjs } from "@mui/x-date-pickers/AdapterDayjs";
import TextField from "@mui/material/TextField";
import Box from "@mui/material/Box";
import { useBooking } from "../contexts/BookingContext";
import dayjs from "dayjs";
import ValidationErrorModal from "./ValidationErrorModal";
import CountryStateDropdown from "./CountryStateDropdown";
import validateForm from "../utils/validateForm";


const BookingForm = () => {
    const {selectedDates, selectedRoom} = useBooking();
    const [firstName, setFirstName] = useState("");
    const [lastName, setLastName] = useState("");
    const [email, setEmail] = useState("");
    const [phoneNumber, setPhoneNumber] = useState("");
    const [city, setCity] = useState("");
    const [streetAddress, setStreetAddress] = useState("");
    const [zipCode, setZipCode] = useState("");
    const [secondGuest, setSecondGuest] = useState(false);
    const [secondFirstName, setSecondFirstName] = useState("");
    const [secondLastName, setSecondLastName] = useState("");
    const [secondEmail, setSecondEmail] = useState("");
    const [additionalCost, setAdditionalCost] = useState(0);
    const [subtotal, setSubtotal] = useState(0);
    const [total, setTotal] = useState(0);
    const [highlightFields, setHighlightFields] = useState(false);
    const [formErrors, setFormErrors] = useState({});
    const [showValidationErrorModal, setShowValidationErrorModal] = useState(false);
    const [country, setCountry] = useState("");
    const [state, setState] = useState("");
    const router = useRouter();
    useEffect(() => {
        if (selectedDates.length === 2 && selectedRoom) {
            const days = dayjs(selectedDates[1]).diff(dayjs(selectedDates[0]), "day");
            const subtotalAmount = days * selectedRoom.rate;
            setSubtotal(subtotalAmount);
        }
    }, [selectedDates, selectedRoom]);
    useEffect(() => {
        let additionalCostAmount = 0;


        setAdditionalCost(additionalCostAmount);
        const processingFee = subtotal * 0.026;
        const totalAmount = (subtotal + processingFee) * 1.06;
        setTotal(totalAmount);
    }, [subtotal, secondGuest]);
    const handleSubmit = async (e) => {
        e.preventDefault();
        if (!validateForm()) {
            setHighlightFields(true);
            setShowValidationErrorModal(true);
            return;
        }
        const handleSubmit = async (e) => {
            e.preventDefault();
            const formData = {
                firstName,
                lastName,
                email,
                phoneNumber,
                country,
                state,
                city,
                streetAddress,
                zipCode,
                secondGuest,
                secondFirstName,
                secondLastName,
                secondEmail,
            };
            const errors = validateForm(formData);
            if (Object.keys(errors).length > 0) {
                setFormErrors(errors);
                setHighlightFields(true);
                setShowValidationErrorModal(true);
                return;
            }
            const bookingData = {
                ...formData,
                startDate: selectedDates[0].toISOString(),
                endDate: selectedDates[1].toISOString(),
                roomId: selectedRoom.id,
                paymentStatus: "pending",
            };
            try {
                const response = await fetch("/api/create-booking", {
                    method: "POST",
                    headers: {
                        "Content-Type": "application/json",
                    },
                    body: JSON.stringify(requestData),
                });
                if (response.ok) {
                    const result = await response.json();
                    router.push(`/payment?bookingId=${result.bookingId}`);
                } else {
                    const errorText = await response.text();
                    console.error("Failed to save booking:", errorText);
                }
            } catch (error) {
                console.error("Error during booking submission:", error);
            }
        };



        const handleValidationErrorModalClose = () => {
            setShowValidationErrorModal(false);
        };
        const handleCountryChange = (e) => {
            setCountry(e.target.value);
            setState("");
        };
        const handleStateChange = (e) => {
            setState(e.target.value);
        };
        return (
            <div className="flex flex-col justify-center items-center">
                <h1 className="text-4xl my-12">Complete your booking details</h1>
                <LocalizationProvider dateAdapter={AdapterDayjs}>
                    <Box mb={2} className="flex justify-center items-center">
                        <TextField
                            label="Start Date"
                            value={
                                selectedDates[0] ? selectedDates[0].format("YYYY-MM-DD") : ""
                            }
                            InputProps={{readOnly: true}}
                        />
                        <Box sx={{mx: 2}}>to</Box>
                        <TextField
                            label="End Date"
                            value={
                                selectedDates[1] ? selectedDates[1].format("YYYY-MM-DD") : ""
                            }
                            InputProps={{readOnly: true}}
                        />
                    </Box>
                    <form
                        onSubmit={handleSubmit}
                        className="flex flex-col lg:w-full mx-auto p-6 bg-white shadow-md rounded-lg"
                    >
                        <h1 className="text-2xl font-bold mb-4">Contact Information</h1>

                        <input
                            type="text"
                            placeholder="First Name"
                            value={firstName}
                            onChange={(e) => setFirstName(e.target.value)}
                            className={`mb-4 p-2 border border-gray-300 rounded ${
                                highlightFields && formErrors.firstName ? "border-red-500" : ""
                            }`}
                        />
                        {highlightFields && formErrors.firstName && (
                            <span className="text-red-500">{formErrors.firstName}</span>
                        )}
                        <input
                            type="text"
                            placeholder="Last Name"
                            value={lastName}
                            onChange={(e) => setLastName(e.target.value)}
                            className={`mb-4 p-2 border border-gray-300 rounded ${
                                highlightFields && formErrors.lastName ? "border-red-500" : ""
                            }`}
                        />
                        {highlightFields && formErrors.lastName && (
                            <span className="text-red-500">{formErrors.lastName}</span>
                        )}
                        <input
                            type="email"
                            placeholder="Email"
                            value={email}
                            onChange={(e) => setEmail(e.target.value)}
                            className={`mb-4 p-2 border border-gray-300 rounded ${
                                highlightFields && formErrors.email ? "border-red-500" : ""
                            }`}
                        />
                        {highlightFields && formErrors.email && (
                            <span className="text-red-500">{formErrors.email}</span>
                        )}
                        <input
                            type="text"
                            placeholder="Phone Number"
                            value={phoneNumber}
                            onChange={(e) => setPhoneNumber(e.target.value)}
                            className={`mb-4 p-2 border border-gray-300 rounded ${
                                highlightFields && formErrors.phoneNumber ? "border-red-500" : ""
                            }`}
                        />
                        {highlightFields && formErrors.phoneNumber && (
                            <span className="text-red-500">{formErrors.phoneNumber}</span>
                        )}

                        <h1 className="text-2xl font-bold mb-4">Address Information</h1>
                        <CountryStateDropdown
                            selectedCountry={country}
                            selectedState={state}
                            handleCountryChange={handleCountryChange}
                            handleStateChange={handleStateChange}
                        />
                        <input
                            type="text"
                            placeholder="City"
                            value={city}
                            onChange={(e) => setCity(e.target.value)}
                            className="mb-4 p-2 border border-gray-300 rounded"
                        />
                        <input
                            type="text"
                            placeholder="Street Address"
                            value={streetAddress}
                            onChange={(e) => setStreetAddress(e.target.value)}
                            className="mb-4 p-2 border border-gray-300 rounded"
                        />
                        <input
                            type="text"
                            placeholder="Zip Code"
                            value={zipCode}
                            onChange={(e) => setZipCode(e.target.value)}
                            className={`mb-4 p-2 border border-gray-300 rounded ${
                                highlightFields && formErrors.zipCode ? "border-red-500" : ""
                            }`}
                        />
                        {highlightFields && formErrors.zipCode && (
                            <span className="text-red-500">{formErrors.zipCode}</span>
                        )}

                        <div className="mb-4">
                            <label className="flex items-center space-x-2">
                                <input
                                    type="checkbox"
                                    checked={secondGuest}
                                    onChange={(e) => setSecondGuest(e.target.checked)}
                                    className="form-checkbox"
                                />
                                <span>Will a second guest be coming?</span>
                            </label>
                        </div>

                        {secondGuest && (
                            <div className="flex flex-col mb-4">
                                <input
                                    type="text"
                                    placeholder="Second Guest First Name"
                                    value={secondFirstName}
                                    onChange={(e) => setSecondFirstName(e.target.value)}
                                    className="mb-4 p-2 border border-gray-300 rounded"
                                />
                                <input
                                    type="text"
                                    placeholder="Second Guest Last Name"
                                    value={secondLastName}
                                    onChange={(e) => setSecondLastName(e.target.value)}
                                    className="mb-4 p-2 border border-gray-300 rounded"
                                />
                                <input
                                    type="email"
                                    placeholder="Second Guest Email"
                                    value={secondEmail}
                                    onChange={(e) => setSecondEmail(e.target.value)}
                                    className="mb-4 p-2 border border-gray-300 rounded"
                                />
                            </div>
                        )}



                        <div className="bg-sky-100 p-4 rounded mb-4">
                            <div className="flex justify-between mb-2">
                                <span className="font-bold">Subtotal:</span>
                                <span>${subtotal.toFixed(2)}</span>
                                {additionalCost > 0 && (
                                    <div className="flex justify-between mb-2">
                                        <span className="font-bold">Additional Cost:</span>
                                        <span>${additionalCost.toFixed(2)}</span>
                                    </div>
                                )}{" "}
                            </div>
                            <div className="flex justify-between mb-2">
                                <span className="font-bold">Sales Tax (6%):</span>
                                <span>
                $
                                    {(
                                        (subtotal +
                                            (100 * (secondGuest ? 2 : 1) : 0) *
                                        0.06
                                    ).toFixed(2))}
              </span>
                            </div>
                            <div className="flex justify-between mb-2">
                                <span className="font-bold">Processing Fee:</span>
                                <span>${(subtotal * 0.026).toFixed(2)}</span>
                            </div>
                            <div className="flex justify-between font-bold text-lg">
                                <span>Total:</span>
                                <span>${total.toFixed(2)}</span>
                            </div>
                        </div>

                        <button
                            type="submit"
                            className="bg-blue-500 text-white py-2 px-4 rounded hover:bg-blue-600 transition duration-200"
                        >
                            Next
                        </button>
                    </form>
                </LocalizationProvider>


                <ValidationErrorModal
                    open={showValidationErrorModal}
                    onClose={handleValidationErrorModalClose}
                    errors={formErrors}
                />
            </div>
        );
    };
}
export default BookingForm;
