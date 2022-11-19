import Display from "../../common/components/Display/Display";
import {Col, Container, Row} from "react-bootstrap";
import AppButton from "../../common/components/AppButton/AppButton";
import "./QuizSettings.scss";
import React, {SyntheticEvent, useState} from "react";
import {getQuiz, QuizTestData} from "../../common/services/quiz-service";

interface QuizSettingsState {
  testCount: number;
  optionCount: number;
  language: LanguageOption;
}

interface LanguageOption {
  to: string;
  from: string;
}

interface QuizSettingsProps {
  setTests: (tests: QuizTestData[]) => void;
}

const QuizSettings = ({setTests}: QuizSettingsProps) => {
  const languagePairs = [{first: "English", second: "Russian"}];

  const [state, setState] = useState<QuizSettingsState>({
    testCount: 0,
    optionCount: 0,
    language: {from: languagePairs[0].first, to: languagePairs[0].second},
  });

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
                     type="number"
                     onChange={setTestCount}/>
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
              <input className="setting-input m-3 w-75"
                     type="number"
                     onChange={setOptionCount}/>
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
              <select className="setting-input m-3 w-75"
                      onChange={setLanguage}>
                {languagePairs.map(l =>
                  <>
                    <option>{[l.first, l.second].join("-")}</option>
                    <option>{[l.second, l.first].join("-")}</option>
                  </>)}
              </select>
            </Col>
          </Row>
          <Row className="justify-content-center">
            <AppButton className="w-75">
              Start
              <button onClick={start}/>
            </AppButton>
          </Row>
        </Container>
      </div>
    </>
  );

  function setTestCount(e: SyntheticEvent<HTMLInputElement>) {
    const newState = {
      ...state,
      testCount: parseInt(e.currentTarget.value),
    };
    setState(newState);
  }

  function setOptionCount (e: SyntheticEvent<HTMLInputElement>) {
    const newState = {
      ...state,
      optionCount: parseInt(e.currentTarget.value),
    };
    setState(newState);
  }

  function setLanguage(e: SyntheticEvent<HTMLSelectElement>) {
    const languages = e.currentTarget.value.split("-");
    const newState = {
      ...state,
      language: {from: languages[0], to: languages[1]}
    };
    setState(newState);
  }

  async function start() {
    const tests = await getQuiz({...state.language, ...state});
    if (tests.length === 0) return;
    setTests(tests);
  }
};

export default QuizSettings;
