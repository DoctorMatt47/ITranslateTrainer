import React from "react";
import "./Header.scss";
import { Link } from "react-router-dom";
import { Container, Navbar } from "react-bootstrap";

export default function Header() {
  return (
    <Navbar bg="black-opacity-25" variant="dark" expand={false}>
      <Container>
        <Navbar.Brand>
          <Link to="/menu">ITranslateTrainer</Link>
        </Navbar.Brand>
      </Container>
    </Navbar>
  );
};
