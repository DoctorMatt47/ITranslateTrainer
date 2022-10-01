import Display from "../../common/components/Display/Display";
import {Col, Container, Row} from "react-bootstrap";
import AppButton from "../../common/components/AppButton/AppButton";

const QuizSettings = () => {
  return (
    <>
      <Display size={4}
               className="display-4 mb-4 text-center"
               text="Quiz settings"/>

      <div className="settings">
        <Container fluid="lg">
          <Row className="align-items-center">
            <Col lg={2}/>
            <Col lg={4} sm={12} className="text-center">
              <label>
                Test count:
              </label>
            </Col>
            <Col lg={4} sm={12} className="text-center">
              <input className="setting-input m-3 w-75"
                     id="test-count"
                     type="number"/>
            </Col>
          </Row>
          <Row className="align-items-center">
            <Col lg={2}/>
            <Col lg={4} sm={12} className="text-center">
              <label>
                Option count:
              </label>
            </Col>
            <Col lg={4} className="text-center">
              <input className="setting-input m-3 w-75" id="option-count"
                     type="number"/>
            </Col>
          </Row>
          <Row className="mb-4 align-items-center">
            <Col lg={2}/>
            <Col lg={4} className="text-center">
              <label>
                Language:
              </label>
            </Col>
            <Col lg={4} className="text-center">
              <select className="setting-input m-3 w-75" id="language-select">
                <option value="12">English-Russian</option>
                <option value="21">Russian-English</option>
              </select>
            </Col>
          </Row>
          <Row className="justify-content-center">
            <AppButton className="w-75">
              Start
              <button id="start-button"></button>
            </AppButton>
          </Row>
        </Container>
      </div>
    </>
  );
}

export default QuizSettings;
