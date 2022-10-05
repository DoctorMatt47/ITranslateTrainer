import QuizSettings from "./Settings/QuizSettings";
import {QuizTestData} from "../common/services/quiz-service";
import {useState} from "react";
import QuizTests from "./Tests/QuizTests";

interface QuizState {
  tests: QuizTestData[];
  isFinished: boolean;
}

const Quiz = () => {
  const [state, setState] = useState<QuizState>({tests: [], isFinished: false});

  if (state.tests.length === 0)
    return <QuizSettings
      setTests={tests => setState({...state, tests: tests})}/>;

  if (!state.isFinished)
    return <QuizTests tests={state.tests}
                      restart={() => setState({...state, tests: []})}
                      finish={() => setState({...state, isFinished: true})}/>;

  return <></>;
}

export default Quiz;
