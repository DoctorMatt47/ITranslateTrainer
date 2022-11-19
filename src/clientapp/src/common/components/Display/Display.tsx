import React from "react";

interface DisplayProps extends React.HTMLAttributes<HTMLDivElement> {
  size: number;
  text: string;
}

const Display = (props: DisplayProps) => {
  const className = [props.className, "display-" + props.size].join(" ");

  return <div className={className}>{props.text}</div>;
};

export default Display;
