<script lang="ts">
  import type { Snippet } from "svelte";

  let { className = "", children }: {
    className?: string;
    children: Snippet;
  } = $props();
</script>

<button class="btn variant-ghost-surface {className}" type="submit">
  {@render children()}
</button>
