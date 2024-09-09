import React from 'react';
import { Routes, Route } from 'react-router-dom';
import Home from './pages/Home';
import Contact from './pages/Contact';
import Rooms from './pages/Rooms';
import CancellationPolicy from './pages/CancellationPolicy';
import BookingForm from './components/BookingForm';

const App = () => {
    return (
        <Routes>
            <Route path="/" element={<Home />} />
            <Route path="/contact" element={<Contact />} />
            <Route path="/rooms" element={<Rooms />} />
            <Route path="/cancellation-policy" element={<CancellationPolicy />} />
            <Route path="/book" element={<BookingForm />} />
        </Routes>
    );
};

export default App;