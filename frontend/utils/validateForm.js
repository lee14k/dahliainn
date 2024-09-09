const validateForm = (formData) => {
    const errors = {};
    const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
    const phoneRegex = /^(\+\d{1,2}\s?)?1?\-?\.?\s?\(?\d{3}\)?[\s\-\.]?\d{3}[\s\-\.]?\d{4}$/;
    const zipCodeRegex = /^\d{5}(-\d{4})?$/;

    if (!formData.firstName.trim()) errors.firstName = "First name is required";
    if (!formData.lastName.trim()) errors.lastName = "Last name is required";
    if (!formData.email.trim()) errors.email = "Email is required";
    else if (!emailRegex.test(formData.email)) errors.email = "Invalid email format";
    if (!formData.phoneNumber.trim()) errors.phoneNumber = "Phone number is required";
    else if (!phoneRegex.test(formData.phoneNumber)) errors.phoneNumber = "Invalid phone number format";
    if (!formData.country) errors.country = "Country is required";
    if (!formData.state) errors.state = "State is required";
    if (!formData.city.trim()) errors.city = "City is required";
    if (!formData.streetAddress.trim()) errors.streetAddress = "Street address is required";
    if (!formData.zipCode.trim()) errors.zipCode = "Zip code is required";
    else if (!zipCodeRegex.test(formData.zipCode)) errors.zipCode = "Invalid zip code format";

    if (formData.secondGuest) {
        if (!formData.secondFirstName.trim()) errors.secondFirstName = "Second guest first name is required";
        if (!formData.secondLastName.trim()) errors.secondLastName = "Second guest last name is required";
        if (!formData.secondEmail.trim()) errors.secondEmail = "Second guest email is required";
        else if (!emailRegex.test(formData.secondEmail)) errors.secondEmail = "Invalid second guest email format";
    }

    return errors;
};

export default validateForm;