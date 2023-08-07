import React from "react";
import { Container } from "react-bootstrap";
import "./Main.scss";

interface MainProps {
  children: React.ReactNode;
}

export default function Main({ children }: MainProps) {
  return (
    <Container fluid="lg" className="mt-5">
      <div className="main d-flex flex-column">
        {children}
      </div>
    </Container>
  );
};
