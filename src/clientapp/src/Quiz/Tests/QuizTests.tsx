import {Col, Container, Row} from "react-bootstrap";
import "./QuizTests.scss";
import AppButton from "../../common/components/AppButton/AppButton";
import {QuizTestData} from "../../common/services/quiz-service";
import {useState} from "react";
import QuizTest, {Result} from "./QuizTest/QuizTest";
import QuizResult, {QuizResultCount} from "./Result/QuizResult";

interface QuizTestsState {
  index: number;
  count: QuizResultCount;
}

interface QuizTestsProps {
  tests: QuizTestData[];
  restart: () => void;
}

const QuizTests = (props: QuizTestsProps) => {
  const [state, setState] = useState<QuizTestsState>({
    index: 0,
    count: {correct: 0, incorrect: 0, skipped: props.tests.length}
  });

  if (state.index !== props.tests.length)
    return (
      <Container className="test">
        <Row>
          <Col lg={3}/>

          <Col lg sm className="text-center">

            <QuizTest test={props.tests[state.index]}
                      key={state.index}
                      setResult={setResult(state, setState)}/>

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

  return <QuizResult count={state.count}/>
}

const setResult = (
  state: QuizTestsState,
  setState: (state: QuizTestsState) => void
) => {
  return (result: Result) => {
    if (result === Result.Skipped) return;

    const newCount = {
      correct: state.count.correct,
      incorrect: state.count.incorrect,
      skipped: state.count.skipped
    }

    if (result === Result.Correct) newCount.correct++;
    if (result === Result.Incorrect) newCount.incorrect++;

    newCount.skipped--;
    setState({...state, count: newCount});
  }
}

const nextTest = (
  props: QuizTestsProps,
  state: QuizTestsState,
  setState: (state: QuizTestsState) => void
) => {
  return () => {
    if (state.index !== props.tests.length) {
      setState({...state, index: state.index + 1})
      return;
    }
  }
}

export default QuizTests;
