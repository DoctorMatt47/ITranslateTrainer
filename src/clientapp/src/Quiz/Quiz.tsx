import QuizSettings from "./Settings/QuizSettings";
import {QuizTestData} from "../common/services/quiz-service";
import {useState} from "react";
import QuizTests from "./Tests/QuizTests";

interface QuizState {
  tests: QuizTestData[];
}

const Quiz = () => {
  const [state, setState] = useState<QuizState>({
    tests: [],
  });

  if (state.tests.length === 0)
    return <QuizSettings
      setTests={tests => setState({...state, tests: tests})}/>;

  return <QuizTests
    tests={state.tests} restart={() => setState({...state, tests: []})}/>;
};

export default Quiz;
