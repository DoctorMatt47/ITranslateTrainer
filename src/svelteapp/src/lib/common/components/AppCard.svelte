<script lang="ts">
  import type { Snippet } from "svelte";

  const { children }: {
    children: Snippet;
  } = $props();
</script>

<div class="card p-8 dark:bg-black/20">
  {@render children()}
</div>
