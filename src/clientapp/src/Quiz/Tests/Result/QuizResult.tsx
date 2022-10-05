import Display from "../../../common/components/Display/Display";
import {Col, Container, Row} from "react-bootstrap";
import AppButton from "../../../common/components/AppButton/AppButton";
import {useEffect} from "react";
import "./QuizResult.scss";

export interface QuizResultCount {
  correct: number;
  incorrect: number;
  skipped: number;
}

interface QuizResultProps {
  count: QuizResultCount;
}

const QuizResult = ({count}: QuizResultProps) => {
  useEffect(() => {
    const circleChart = document.getElementById("circle-chart");
    circleChart!.style.background = conicGradientStyle(count);
  });
  return (
    <>
      <Display size={4} className="text-center" text="Result"/>

      <Container fluid="lg" className="fs-5">
        <div className="mt-3">
          <Col>

            <Row className="row mt-4">
              <Col sm={6} className="text-center">
                Correct:
              </Col>
              <Col sm={2}
                   className="mt-2 mt-sm-0 text-center text-lg-start">
                {count.correct}
              </Col>
            </Row>
            <Row className="mt-4">
              <Col sm={6} className="text-center">
                Incorrect:
              </Col>
              <Col sm={2}
                   className="mt-2 mt-sm-0 text-center text-lg-start">
                {count.incorrect}
              </Col>
            </Row>
            <Row className="mt-4">
              <Col sm={6} className="text-center">
                Skipped:
              </Col>
              <Col sm={2}
                   className="mt-2 mt-sm-0 text-center text-lg-start"
                   id="skipped-count">
                {count.skipped}
              </Col>
            </Row>
            <Row className="mt-4">
              <Col sm={6} className="text-center">
                Percent:
              </Col>
              <Col sm={2}
                   className="mt-2 mt-sm-0 mb-4 text-center text-lg-start">

              </Col>
            </Row>
          </Col>

          <div className="col d-flex justify-content-center align-items-center">
            <div className="circle-chart" id="circle-chart"/>
          </div>

        </div>

        <div className="row justify-content-center mt-5">
          <AppButton label="Restart" className="text-center w-75">
            <button id="restart-button"/>
          </AppButton>
        </div>

      </Container>
    </>
  );
};

const round = (num: number) => Math.round((num + Number.EPSILON) * 100) / 100;

const toDegree = (relative: number, absolute: number) =>
  round(relative / absolute) * 360;

const testCount = (count: QuizResultCount) =>
  count.correct + count.skipped + count.incorrect;

const conicGradientStyle = (count: QuizResultCount) => {
  const diagramRed = "#c10b0c";
  const diagramGreen = "#089e07";
  const diagramGray = "#929292";

  const absolute = testCount(count);

  const correct = toDegree(count.correct, absolute);
  const incorrect = toDegree(count.incorrect, absolute);
  const skipped = toDegree(count.skipped, absolute);

  return `conic-gradient(
      ${diagramGreen} 0deg ${correct}deg,
      ${diagramRed} ${correct}deg ${correct + incorrect}deg,
      ${diagramGray} ${360 - skipped}deg ${skipped}deg)`
}

export default QuizResult;
