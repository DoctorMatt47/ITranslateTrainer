import Display from "common/components/Display/Display";
import {Col, Container, Row} from "react-bootstrap";
import AppButton from "common/components/AppButton/AppButton";
import "./TestSettings.scss";
import React, {useState} from "react";
import {
  answerOnTest,
  getTest,
  putTest,
  PutTestRequest,
  TestResponse
} from "../common/services/test-service";
import {useForm} from "react-hook-form";
import Test from "./Test/Test";

interface QuizSettingsState {
  test: TestResponse | null;
  testRequest: PutTestRequest | null;
  isChosen: boolean;
}

const QuizSettings = () => {
  const {register, handleSubmit, watch} = useForm();
  const [state, setState] = useState<QuizSettingsState>({
    test: null,
    testRequest: null,
    isChosen: false,
  });

  if (state.test !== null)
    return <Container className="test">
      <Row>
        <Col lg={3}/>

        <Col lg sm className="text-center">

          <Test test={state.test} setAnswer={setAnswer} isChosen={state.isChosen}/>

          <Container className="p-0">
            <Row>
              <Col className="text-end">
                <AppButton className="next-button" label="Next">
                  <button onClick={next}/>
                </AppButton>
              </Col>
            </Row>
          </Container>
        </Col>

        <Col lg={3}/>
      </Row>
    </Container>;

  return (
    <>
      <Display size={4}
               className="display-4 mb-4 text-center"
               text="Quiz settings"/>

      <div className="settings">
        <form onSubmit={handleSubmit(start)}>
          <Container fluid="lg">
            <Row className="mb-1 align-items-center">
              <Col lg={2}/>
              <Col lg={4} sm={12} className="text-center">
                <label>
                  Option count:
                </label>
              </Col>
              <Col lg={4} className="text-center">
                <input className="setting-input m-lg-2 mb-sm-3 mt-md-1 w-75"
                       type="number"
                       defaultValue={10}
                       {...register("option-count")}/>
              </Col>
            </Row>
            <Row className="mb-1 align-items-center">
              <Col lg={2}/>
              <Col lg={4} className="text-center">
                <label>
                  From:
                </label>
              </Col>
              <Col lg={4} className="text-center">
                <input id="from"
                       className="setting-input m-lg-2 mb-sm-3 mt-md-1 w-75"
                       type="text"
                       defaultValue={"english"}
                       {...register("from")}/>
              </Col>
            </Row>
            <Row className="mb-4 align-items-center">
              <Col lg={2}/>
              <Col lg={4} className="text-center">
                <label>
                  To:
                </label>
              </Col>
              <Col lg={4} className="text-center">
                <input id="to"
                       className="setting-input m-lg-2 mb-sm-3 mt-md-1 w-75"
                       type="text"
                       defaultValue={"russian"}
                       {...register("to")}/>
              </Col>
            </Row>
            <Row className="justify-content-center">
              <AppButton className="w-75">
                Start
                <input type="submit" className="visually-hidden"/>
              </AppButton>
            </Row>
          </Container>
        </form>
      </div>
    </>
  );

  async function setAnswer(optionId: number) {
    await answerOnTest(state.test!.id, {optionId});
    const response = await getTest(state.test!.id);
    setState({...state, test: response, isChosen: true});
  }

  async function start() {
    setState({ ...state, testRequest: {
        from: watch("from"),
        to: watch("to"),
        optionCount: watch("option-count"),
      }});

    await next();
  }

  async function next() {
    const test = await putTest(state.testRequest!);
    setState({...state, test: test, isChosen: false});
  }
};

export default QuizSettings;
