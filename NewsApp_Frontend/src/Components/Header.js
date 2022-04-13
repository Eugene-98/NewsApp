import React, { Component } from "react";
import { Button, Container, Form, FormControl, Nav, Navbar, NavLink } from "react-bootstrap";
import logo from './logo.png'
import {BrowserRouter as Router, Routes, Route, Link} from 'react-router-dom';
import Home from "../Pages/Home";
import Allnews from "../Pages/Allnews";
import AdminPage from "../Pages/AdminPage";

export default class Header extends Component {
    render() {
        return (
            <>
            <Navbar collapseOnSelect expand="md" bg="dark" variant="dark">
                <Container>
                    <Navbar.Brand href="/">
                        <img
                            src={logo}
                            height="30"
                            width="30"
                            className="d-inline-block align-top"
                            alt="Logo"
                        />
                    </Navbar.Brand>
                    <Navbar.Toggle aria-controls="responsive-navbar-nav"/>
                    <Navbar.Collapse id="responsive-navbar-nav">
                        <Nav className="me-auto">
                            <NavLink href="/">Home</NavLink>
                            <NavLink href="/allnews">All News</NavLink>
                            <NavLink href="/adminPage">Admin</NavLink>
                        </Nav>
                        <Form className="d-flex">
                            <FormControl
                                type="text"
                                placeholder="Search"
                                className="d-inline mx-2"
                            />
                            <Button variant="outline-info">Search</Button>
                        </Form>
                    </Navbar.Collapse>
                </Container>
            </Navbar>
            
            <Router>
                <Routes>
                    <Route path="/" element={<Home/>}/>
                    <Route path="/allnews" element={<Allnews/>}/>
                    <Route path="/adminPage" element={<AdminPage/>}/>
                </Routes>
            </Router>
            </>
        );
    }
}