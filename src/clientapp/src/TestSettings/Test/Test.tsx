import Display from "common/components/Display/Display";
import Option, {OptionType} from "./Option/Option";
import {useEffect, useState} from "react";
import {getTest, TestResponse} from "common/services/test-service";

export enum Result {
  Skipped,
  Correct,
  Incorrect,
}

interface TestState extends TestResponse {
}

interface QuizTestProps {
  test: TestResponse
}

const Test = ({test}: QuizTestProps) => {
  useEffect(() => {
    },
    []);

  const [state, setState] = useState<TestState>({
    id: 0,
    string: "",
    options: [],
  });

  return (
    <>
      <Display size={5} className="display-5 mb-4" text={test.string}/>
      <form className="options d-flex flex-column"
            onChange={() => {
            }}>
        {test.options.map(o =>
          <Option className="mb-4"
                  key={o.id}
                  text={o.string}
                  type={OptionType.Unknown}/>)}
      </form>
    </>
  );


  async function getResult() {
    const test = await getTest(state.id);
    setState(test);
  }

  // function optionsChangeHandler(e: SyntheticEvent<HTMLFormElement>) {
  //   const optionElements = e.currentTarget.elements;
  //
  //   for (let i = 0; i < optionElements.length; i++) {
  //     const optionElement = optionElements[i] as HTMLInputElement;
  //     const option = test.options[i];
  //
  //     state.options[i].type = optionType(
  //       option.isCorrect,
  //       optionElement.checked);
  //   }
  //
  //   setResult(getTestResult(state.options));
  //   setState({...state});
  //
  //   function optionType(isCorrect: boolean, isChosen: boolean): OptionType {
  //     if (isCorrect && isChosen) return OptionType.CorrectChosen;
  //     if (isCorrect && !isChosen) return OptionType.CorrectNotChosen;
  //     if (!isCorrect && isChosen) return OptionType.IncorrectChosen;
  //     return OptionType.Unknown;
  //   }
  //
  //   function getTestResult(options: any): Result {
  //     for (const option of options) {
  //       switch (option.type) {
  //         case OptionType.CorrectChosen:
  //           return Result.Correct;
  //         case OptionType.IncorrectChosen:
  //           return Result.Incorrect;
  //       }
  //     }
  //     return Result.Skipped;
  //   }
  // }
};

export default Test;
