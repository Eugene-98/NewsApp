import React, { Component } from "react";
import { Button, Container, Form, FormControl, Nav, Navbar, NavLink, Row, Col, Card, Image } from "react-bootstrap";
import logo from './logo.png'
import {BrowserRouter as Router, Routes, Route, Link, useOutletContext, useParams} from 'react-router-dom';
import Home from "../Pages/Home";
import News from "../Pages/Allnews";
import { variables } from '../Variables';


function Details(){
    const news = useOutletContext();
    let {newsId} = useParams();
    const news1 = news.find(n=>n.id == newsId);
    if (news1===undefined)
        return <h2>Not Founded</h2>;
    else
        return(
            <img src={variables.FILES_URL+news1.NewsImagePath}/>
        );
}

function NewsList(){
    const news = useOutletContext();
    return (
    <div>
        {news.map(n =>
            <Container fluid="md" key={n.NewsId}>
                <Row >
                <Col>
                <Image src={variables.FILES_URL+n.NewsImagePath}/>
                </Col>
                <Col>
                <Card style={{ width: '100%' }}>
                    <Card.Body>
                        <Card.Title>{n.NewsHeader}</Card.Title>
                        <Card.Text>{n.NewsSubtitle}</Card.Text>
                        <NavLink href={'/allnews/'+n.NewsId}>More</NavLink>
                    </Card.Body>
                </Card>
                </Col>
                </Row>
            </Container>
            )}
    </div>
    )
}
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
                    <Route path="/allnews" element={<News/>}>
                        <Route index element={<NewsList/>}/>
                        <Route path=":id" element={<Details/>}/>
                    </Route>
                </Routes>
            </Router>
            </>
        );
    }
}