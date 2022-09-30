import {ChildrenProps} from "../../../interfaces/props";
import {Container} from "react-bootstrap";
import "./Main.scss"

const Main = ({children}: ChildrenProps) => {
  return (
    <Container fluid="lg" className="mt-5">
      <div className="main d-flex flex-column">
        {children}
      </div>
    </Container>
  );
}

export default Main;
