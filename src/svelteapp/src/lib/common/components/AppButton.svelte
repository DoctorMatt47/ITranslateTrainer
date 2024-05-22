<script lang="ts">
  import type { Snippet } from "svelte";
  import type { MouseEventHandler } from "svelte/elements";

  const { className = "", children, onclick }: {
    className?: string;
    children: Snippet;
    onclick: MouseEventHandler<HTMLButtonElement>;
  } = $props();
</script>

<button class="btn variant-ghost-surface {className}" {onclick} type="button">
  {@render children()}
</button>
