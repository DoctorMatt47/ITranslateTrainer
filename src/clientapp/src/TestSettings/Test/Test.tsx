import Display from "common/components/Display/Display";
import Option, {OptionType} from "./Option/Option";
import {SyntheticEvent, useEffect, useState} from "react";
import {
  answerOnTest,
  getTest,
  TestResponse
} from "common/services/test-service";

export enum Result {
  Skipped,
  Correct,
  Incorrect,
}

interface QuizTestProps {
  test: TestResponse;
  isChosen: boolean;
  setAnswer: (optionId : number) => void;
}

const Test = ({test, isChosen, setAnswer}: QuizTestProps) => {
  return (
    <>
      <Display size={5} className="display-5 mb-4" text={test.string}/>
      <form className="options d-flex flex-column"
            onChange={optionChosen}>
        {test.options.map(o =>
          <Option className="mb-4"
                  key={o.id}
                  optionId={o.id}
                  text={o.string}
                  type={optionType(o.isCorrect, o.isChosen)}/>)}
      </form>
    </>
  );

  async function optionChosen(e: SyntheticEvent<HTMLFormElement>) {
    if (isChosen) return;

    const chosen = Array.from(e.currentTarget.elements)
      .map(x => x as HTMLInputElement)
      .find(x => x.checked);

    setAnswer(parseInt(chosen!.value));
  }

  function optionType(isCorrect: boolean, isChosen: boolean): OptionType {
    if (isCorrect && isChosen) return OptionType.CorrectChosen;
    if (isCorrect && !isChosen) return OptionType.CorrectNotChosen;
    if (!isCorrect && isChosen) return OptionType.IncorrectChosen;
    return OptionType.Unknown;
  }
};

export default Test;
