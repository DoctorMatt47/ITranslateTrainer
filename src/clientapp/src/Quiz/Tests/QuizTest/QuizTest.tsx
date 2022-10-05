import Display from "../../../common/components/Display/Display";
import QuizOption, {OptionType} from "./QuizTestOption/QuizOption";
import {QuizTestData} from "../../../common/services/quiz-service";
import {SyntheticEvent, useState} from "react";

enum Result {
  Skipped,
  Correct,
  Incorrect,
}

interface QuizTestProps {
  test: QuizTestData;
}

interface QuizOptionState {
  text: string;
  type: OptionType
}

interface QuizTestState {
  options: QuizOptionState[];
  result: Result;
}

const QuizTest = ({test}: QuizTestProps) => {
  const [state, setState] = useState<QuizTestState>({
    options: test.options.map(o => ({
      text: o.string,
      type: OptionType.Unknown
    })),
    result: Result.Skipped
  });

  return (
    <>
      <Display size={5} className="display-5 mb-4" text={test.string}/>
      <form className="options d-flex flex-column"
            onChange={optionsChangeHandler(test, state, setState)}>
        {state.options.map(o => <QuizOption className="mb-4"
                                            key={o.text}
                                            text={o.text}
                                            type={o.type}/>)}
      </form>
    </>
  );
}

const optionsChangeHandler = (
  test: QuizTestData,
  state: QuizTestState,
  setState: (state: QuizTestState) => void
) => {
  return (e: SyntheticEvent<HTMLFormElement>) => {
    const optionElements = e.currentTarget.elements;

    for (let i = 0; i < optionElements.length; i++) {
      const optionElement = optionElements[i] as HTMLInputElement;
      const option = test.options[i];

      state.options[i].type = optionType(
        option.isCorrect,
        optionElement.checked);
    }

    state.result = getTestResult(state.options);
    setState({...state});
  }
};

const optionType = (isCorrect: boolean, isChosen: boolean): OptionType => {
  if (isCorrect && isChosen) return OptionType.CorrectChosen;
  if (isCorrect && !isChosen) return OptionType.CorrectNotChosen;
  if (!isCorrect && isChosen) return OptionType.IncorrectChosen;
  return OptionType.Unknown;
}

const getTestResult = (options: QuizOptionState[]): Result => {
  for (const option of options) {
    if (option.type === OptionType.CorrectChosen) return Result.Correct;
    if (option.type === OptionType.IncorrectChosen) return Result.Incorrect;
  }
  return Result.Skipped;
}

export default QuizTest;
