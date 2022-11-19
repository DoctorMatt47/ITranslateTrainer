import Display from "../../../common/components/Display/Display";
import QuizOption, {OptionType} from "./QuizTestOption/QuizOption";
import {QuizTestData} from "../../../common/services/quiz-service";
import {SyntheticEvent, useState} from "react";

interface QuizTestState {
  options: QuizOptionState[];
}

interface QuizOptionState {
  text: string;
  type: OptionType
}

interface QuizTestProps {
  test: QuizTestData;
  setResult: (result: Result) => void;
}

export enum Result {
  Skipped,
  Correct,
  Incorrect,
}

const QuizTest = ({test, setResult}: QuizTestProps) => {
  const [state, setState] = useState<QuizTestState>({
    options: test.options.map(o => ({
      text: o.string,
      type: OptionType.Unknown
    }))
  });

  return (
    <>
      <Display size={5} className="display-5 mb-4" text={test.string}/>
      <form className="options d-flex flex-column"
            onChange={optionsChangeHandler}>
        {state.options.map(o =>
          <QuizOption className="mb-4"
                      key={o.text}
                      text={o.text}
                      type={o.type}/>)}
      </form>
    </>
  );

  function optionsChangeHandler(e: SyntheticEvent<HTMLFormElement>) {
    const optionElements = e.currentTarget.elements;

    for (let i = 0; i < optionElements.length; i++) {
      const optionElement = optionElements[i] as HTMLInputElement;
      const option = test.options[i];

      state.options[i].type = optionType(
        option.isCorrect,
        optionElement.checked);
    }

    setResult(getTestResult(state.options));
    setState({...state});

    function optionType(isCorrect: boolean, isChosen: boolean): OptionType {
      if (isCorrect && isChosen) return OptionType.CorrectChosen;
      if (isCorrect && !isChosen) return OptionType.CorrectNotChosen;
      if (!isCorrect && isChosen) return OptionType.IncorrectChosen;
      return OptionType.Unknown;
    }

    function getTestResult(options: QuizOptionState[]): Result {
      for (const option of options) {
        switch (option.type) {
          case OptionType.CorrectChosen:
            return Result.Correct;
          case OptionType.IncorrectChosen:
            return Result.Incorrect;
        }
      }
      return Result.Skipped;
    }
  }
};

export default QuizTest;
