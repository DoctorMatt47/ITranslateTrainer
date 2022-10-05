import {Col, Container, Row} from "react-bootstrap";
import "./QuizTests.scss";
import AppButton from "../../common/components/AppButton/AppButton";
import {QuizTestData} from "../../common/services/quiz-service";
import {useState} from "react";
import QuizTest from "./QuizTest/QuizTest";

interface QuizTestsProps {
  tests: QuizTestData[];
  restart: () => void;
  finish: () => void;
}

interface QuizTestsState {
  index: number;
}

const QuizTests = (props: QuizTestsProps) => {
  const [state, setState] = useState<QuizTestsState>({index: 0});

  return (
    <Container className="test">
      <Row>
        <Col lg={3}/>

        <Col lg sm className="text-center">

          <QuizTest test={props.tests[state.index]} key={state.index}/>

          <Container className="p-0">
            <Row>
              <Col>
                <AppButton className="next-button" label="Restart">
                  <button onClick={props.restart}/>
                </AppButton>
              </Col>

              <Col className="text-end">
                <AppButton className="next-button" label="Next">
                  <button onClick={nextTest(props, state, setState)}/>
                </AppButton>
              </Col>
            </Row>
          </Container>
        </Col>

        <Col lg={3}/>
      </Row>
    </Container>
  );
}

const nextTest = (
  props: QuizTestsProps,
  state: QuizTestsState,
  setState: (state: QuizTestsState) => void
) => {
  return () => {
    if (state.index !== props.tests.length) {
      setState({index: state.index + 1})
      return;
    }
    props.finish();
  }
}

export default QuizTests;
