<script lang="ts">
  import type { Snippet } from "svelte";

  const { className = "", children, ...restProps }: {
    className?: string;
    children: Snippet;
    [key: string]: unknown;
  } = $props();
</script>

<button {...restProps} class="btn variant-ghost-surface {className}" type="button">
  {@render children()}
</button>
